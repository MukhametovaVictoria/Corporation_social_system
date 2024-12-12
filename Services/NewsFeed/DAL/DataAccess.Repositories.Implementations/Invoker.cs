using System;
using System.Linq;
using System.Reflection;

namespace DataAccess.Repositories
{
    public class Invoker
    {
        #region Private fields

        private string _classPath;
        private string _methodName;
        private bool _isGenericMethod;
        private Tuple<Type, object>[] _constructorParameters;
        private Tuple<Type, object>[] _methodParameters;
        private Type[] _genericMethodTypes;

        #endregion

        #region Public properties

        /// <summary>
        /// Путь до класса
        /// </summary>
        public string ClassPath { get => _classPath; set => _classPath = value; }
        /// <summary>
        /// Метод класса
        /// </summary>
        public string MethodName { get => _methodName; set => _methodName = value; }
        /// <summary>
        /// Параметры конструктора <Тип, значение>
        /// </summary>
        public Tuple<Type, object>[] ConstructorParameters { get => _constructorParameters; set => _constructorParameters = value; }
        /// <summary>
        /// Параметры метода <Тип, значение>
        /// </summary>
        public Tuple<Type, object>[] MethodParameters { get => _methodParameters; set => _methodParameters = value; }
        /// <summary>
        /// Метод является дженериком
        /// </summary>
        public bool IsGenericMethod { get => _isGenericMethod; set => _isGenericMethod = value; }
        /// <summary>
        /// Типы дженерик метода
        /// </summary>
        public Type[] GenericMethodTypes { get => _genericMethodTypes; set => _genericMethodTypes = value; }

        #endregion

        public Invoker(string classPath, string methodName, bool isGenericMethod, Tuple<Type, object>[] constrParams = null, Tuple<Type, object>[] methodParams = null, Type[] genericParams = null)
        {
            ClassPath = classPath;
            MethodName = methodName;
            ConstructorParameters = constrParams;
            MethodParameters = methodParams;
            GenericMethodTypes = genericParams;
            IsGenericMethod = isGenericMethod;
        }

        #region Public methods

        /// <summary>
        /// Вызов метода класса
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object InvokeMethod()
        {
            if (string.IsNullOrEmpty(ClassPath) || string.IsNullOrEmpty(MethodName))
                return null;

            var classType = Type.GetType(ClassPath, false, false);

            if (classType == null)
            {
                throw new Exception("Класс по пути '" + ClassPath + "' не найден");
            }

            MethodInfo[] methodArr = classType.GetMethods();

            if (methodArr == null || methodArr.Length == 0)
                return null;

            var methods = methodArr.Where(m => m.Name == MethodName && m.IsGenericMethod == IsGenericMethod).ToArray();

            if (methods == null || methods.Length == 0)
            {
                throw new Exception("Метод '" + MethodName + "' не найден в классе '" + classType.Name + "'");
            }

            var constructors = classType.GetConstructors();
            if (constructors == null || constructors.Length == 0)
                throw new Exception("Не найден конструктор в классе '" + classType.Name + "'");

            object handler = null;

            //Проходим по конструкторам, пытаемся подобрать подходящий в зависимости от параметров
            foreach (var constr in constructors)
            {
                try
                {
                    //Считываем какие принимает параметры конструктор
                    var parameters = constr.GetParameters();
                    if (CheckMethod(ConstructorParameters, parameters))
                    {
                        var constrParameters = ConstructorParameters != null && ConstructorParameters.Length > 0 ? ConstructorParameters.Select(x => x.Item2).ToArray() : new object[] { };
                        handler = constr.Invoke(constrParameters);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            if (handler == null)
                throw new Exception("Не найден подходящий конструктор в классе '" + classType.Name + "'");

            //Пытаемся подобрать подходящий метод, если имеется несколько перегрузок
            foreach (var method in methods)
            {
                try
                {
                    var parametersOfMethod = method.GetParameters();
                    if (CheckMethod(MethodParameters, parametersOfMethod))
                    {
                        var parameters = MethodParameters != null && MethodParameters.Length > 0 ? MethodParameters.Select(x => x.Item2).ToArray() : new object[] { };
                        if (method.IsGenericMethod && IsGenericMethod)
                        {
                            var genericTypes = method.GetGenericArguments();
                            if (CheckTypeNameOfParams(genericTypes, GenericMethodTypes))
                            {
                                var methInfo = method.MakeGenericMethod(GenericMethodTypes);
                                return methInfo.Invoke(handler, parameters);
                            }
                        }
                        else if (!method.IsGenericMethod && !IsGenericMethod)
                            return method.Invoke(handler, parameters);
                        else
                            continue;
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return null;
        }

        /// <summary>
        /// Проверка массивов типов дженерик метода. Сравнение входящих типов с переданным массивом
        /// </summary>
        /// <param name="paramTypes">Типы параметров метода</param>
        /// <param name="incomParamTypes">Входящие типы параметров</param>
        /// <returns></returns>
        public bool CheckTypeNameOfParams(Type[] paramTypes, Type[] incomParamTypes)
        {
            var names = paramTypes.Select(x => x.Name).ToList();
            var incomNames = incomParamTypes.Select(x => x.Name).ToList();
            foreach (var className in incomNames)
            {
                if (!names.Contains(className))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка метода на соответствие
        /// </summary>
        /// <param name="incomingParams">Входящие параметры метода</param>
        /// <param name="parametersOfMethod">Параметры проверяемого метода</param>
        /// <returns></returns>
        public bool CheckMethod(Tuple<Type, object>[] incomingParams, ParameterInfo[] parametersOfMethod)
        {
            if ((incomingParams == null || incomingParams.Length == 0) && parametersOfMethod.Length == 0)
                return true;
            else if (incomingParams != null && incomingParams.Length > 0 && parametersOfMethod.Length == 0)
                return false;
            else
            {
                var notDefaultParams = parametersOfMethod.Where(x => !x.HasDefaultValue);

                if (notDefaultParams.Count() <= incomingParams.Length)
                    return CheckParameters(incomingParams.Select(x => x.Item1).ToArray(), parametersOfMethod.Select(x => x.ParameterType).ToArray());
            }

            return false;
        }

        /// <summary>
        /// Проверка параметров метода
        /// </summary>
        /// <param name="incomParamTypes">Входящие типы параметров</param>
        /// <param name="paramTypes">Типы параметров проверяемого метода</param>
        /// <returns></returns>
        public bool CheckParameters(Type[] incomParamTypes, Type[] paramTypes)
        {
            foreach (var incomParamType in incomParamTypes)
            {
                if (incomParamType.IsGenericType)
                {
                    //Возврат результата только в случае false, который эквивалентен прерыванию цикла.
                    //Иначе продолжаем выполнение цикла с проверками
                    if (!CheckGenericParams(incomParamType, paramTypes))
                        return false;
                }
                else
                {
                    //Возврат результата только в случае false, который эквивалентен прерыванию цикла.
                    //Иначе продолжаем выполнение цикла с проверками
                    if (!CheckTypeNameOfParams(paramTypes, new[] { incomParamType }))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Проверка наличия входящего дженерик параметра в массиве параметров метода
        /// </summary>
        /// <param name="incomParamType">Входящий тип параметра</param>
        /// <param name="paramTypes">Типы параметров проверяемого метода</param>
        /// <returns></returns>
        public bool CheckGenericParams(Type incomParamType, Type[] paramTypes)
        {
            //Получили только дженерик типы со сверкой по названию типа (название вида List` и т.п.)
            var genericParamTypesOfMethod = paramTypes.Where(x => x.IsGenericType && x.Name == incomParamType.Name).ToList();

            //В проверяемом списке paramTypes нет дженерик параметров, похожих на входящий параметр (incomParamType)
            if (genericParamTypesOfMethod == null || genericParamTypesOfMethod.Count == 0)
                return false;

            //Вычленяем аргументы входящего дженерик типа, например в List<Guid> это Guid
            var incomGenericTypeArgs = incomParamType.GenericTypeArguments;

            var hasAppropriateParameter = false;
            foreach (var genericParamTypeOfMethod in genericParamTypesOfMethod)
            {
                //Получили список аргументов дженерик параметра метода
                var args = genericParamTypeOfMethod.GenericTypeArguments;

                //Несовпадение количества аргументов дженерика
                if (args.Length != incomGenericTypeArgs.Length)
                    continue;

                var allArgsAreAppropriate = true;
                //Проходим по каждому аргументу
                for (var i = 0; i < incomGenericTypeArgs.Length; i++)
                {
                    //Аргументы оба являются дженериками. Рекурсия
                    if (incomGenericTypeArgs[i].IsGenericType && args[i].IsGenericType)
                    {
                        if (!CheckGenericParams(incomGenericTypeArgs[i], new[] { args[i] }))
                        {
                            allArgsAreAppropriate = false;
                            break;
                        }
                    }
                    else if (!incomGenericTypeArgs[i].IsGenericType && !args[i].IsGenericType) //Оба аргумента не дженерики
                    {
                        //Сверка типов
                        if (!CheckTypeNameOfParams(new[] { args[i] }, new[] { incomGenericTypeArgs[i] }))
                        {
                            allArgsAreAppropriate = false;
                            break;
                        }
                    }
                    else //Аргументы не сопали по типу, значит параметр метода не подходит
                    {
                        allArgsAreAppropriate = false;
                        break;
                    }
                }

                //Все аргументы дженерик параметра совпали
                if (allArgsAreAppropriate)
                {
                    hasAppropriateParameter = true;
                    break;
                }
            }

            //Если не нашлось подходящего параметра в методе, который соответствует incomParamType, то прерываем
            if (!hasAppropriateParameter)
                return false;

            return true;
        }

        #endregion
    }
}
