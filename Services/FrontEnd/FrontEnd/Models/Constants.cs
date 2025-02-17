﻿namespace FrontEnd.Models
{
    public static class Constants
    {
        public static readonly string LanguageBase = "ru-RU";
        public static readonly string FullNamePrefix = "_FullName";
        public static readonly string LanguagePrefix = "_Language";
        public static readonly string ObjectPrefix = "_Object";
        public static readonly string IsAdminPrefix = "_IsAdmin";
        public static readonly string NewsFeedListViewDataKey = "NewsList";
        public static readonly string UserFullNameKey = "UserFullName";
        public static readonly string PersonalAccountDataKey = "PersonalAccountData";
        public static readonly string UserIdCookieKey = "userId";
        public static readonly string NewsListViewModelKey = "NewsListViewModel";
        public static readonly string CaptionsKey = "Captions";
        public static readonly string EmojiViewModelListKey = "EmojiViewModelList";
        public static readonly string CreateNewsViewModelKey = "CreateNewsViewModel";
        public static readonly string LangRUCaption = "RU";
        public static readonly string LangENCaption = "EN";
        public static readonly string RuRULangCaption = "ru-RU";
        public static readonly string EnUSLangCaption = "en-US";
        public static readonly string LanguageKey = "Langs";
		public static readonly string TimeTrackingDataKey = "TimeTrackingData";
		public static readonly string ProjectsDataKey = "ProjectsData";
        public static readonly string AllProjectsDataKey = "AllProjectsData";
        public static readonly string WeekDaysDataKey = "WeekDaysData";
        public static readonly string WeeksDataKey = "WeeksData";
        public static readonly string CurrentTimesheetStartKey = "CurrentTimesheetStart";
        public static readonly string CurrentTimesheetTillKey = "CurrentTimesheetTill";
        public static readonly string MonthsKey = "Months";
        public static readonly string PrevWeekKey = "PrevWeek";
        public static readonly string NextWeekKey = "NextWeek";
        public static readonly string PrevTwoWeeksKey = "PrevTwoWeeks";
        public static readonly string NextTwoWeeksKey = "NextTwoWeeks";
        public static readonly string TimeSheetDataKey = "TimeSheetData";
        public static readonly string LoginKey = "LoginValue";
        public static readonly string IsAdminKey = "IsAdmin";

        public static readonly Dictionary<string, object> Dictionaries = new Dictionary<string, object>()
        {
            { "ru-RU", new CaptionsBase() },
            { "en-US", new CaptionsEN() }
        };

        public static readonly Dictionary<string, string> Langs = new Dictionary<string, string>()
        {
            { "ru-RU", LangRUCaption },
            { "en-US", LangENCaption }
        };
    }

    public class CaptionsBase
    {
        public virtual string CorporationSocialSystem { get; } = "Корпоративная социальная система";
        public virtual string TitleHomeCaption { get; } = "Главная";
        public virtual string PageTitleHomeCaption { get; } = "Корпоративная социальная система";
        public virtual string GoToNewsCaption { get; } = "Перейти к новостям";
        public virtual string TitleNewsCaption { get; } = "Новости";
        public virtual string PageTitleNewsCaption { get; } = "Новости компании";
        public virtual string PageTitleSearchNewsCaption { get; } = "Поиск по новостям";
        public virtual string PageTitleCreatingNewsCaption { get; } = "Создание новости";
        public virtual string GoToCreatingNewsCaption { get; } = "Перейти к созданию новости";
        public virtual string SearchPlainFormTitleCaption { get; } = "Тема (частично)";
        public virtual string SearchPlainFormAuthorNameCaption { get; } = "Имя автора";
        public virtual string SearchPlainFormAuthorSurnameCaption { get; } = "Фамилия автора";
        public virtual string SearchPlainFormCreatedAtStartCaption { get; } = "Дата создания С";
        public virtual string SearchPlainFormCreatedAtTillCaption { get; } = "Дата создания ПО";
        public virtual string SearchPlainFormHashtagsCaption { get; } = "Хэштеги (через пробел без решетки)";
        public virtual string SearchButtonCaption { get; } = "Искать";
        public virtual string MoreButtonCaption { get; } = "Подробнее";
        public virtual string HideButtonCaption { get; } = "Скрыть";
        public virtual string LoadMoreCaption { get; } = "Больше";
        public virtual string CreateNewsCaption { get; } = "Создать новость";
        public virtual string SearchNewsCaption { get; } = "Поиск";
        public virtual string WideSearchNewsCaption { get; } = "Расширенный поиск";
        public virtual string CreatingNewsTitleCaption { get; } = "Тема";
        public virtual string CreatingNewsShortDescriptionCaption { get; } = "Краткое описание";
        public virtual string CreatingNewsContentCaption { get; } = "Описание";
        public virtual string CreatingNewsHashtagsCaption { get; } = "Хэштеги (через пробел без решетки)";
        public virtual string CreateButtonCaprion { get; } = "Создать";
        public virtual string TitleCreatedNewsCaption { get; } = "Новость успешно создана";
        public virtual string PageTitleCreatedNewsCaption { get; } = "Новость создана";
        public virtual string ItWillBeShownAfterModerationCaption { get; } = "После прохождения модерации она отобразится в ленте новостей.";
        public virtual string TitleDeletedNewsCaption { get; } = "Новость успешно удалена";
        public virtual string PageTitleDeletedNewsCaption { get; } = "Новость удалена";
        public virtual string TitleUpdatedNewsCaption { get; } = "Новость успешно обновлена";
        public virtual string PageTitleUpdatedNewsCaption { get; } = "Новость обновлена";
        public virtual string TitleFailedCreatingNewsCaption { get; } = "Ошибка при создании новости";
        public virtual string PageTitleFailedCreatingNewsCaption { get; } = "Ошибка";
        public virtual string ErrorTillCreatingNewsCaption { get; } = "Во время создания новости произошла ошибка. Попробуйте позднее.";
        public virtual string TitleFailedDeletingNewsCaption { get; } = "Ошибка при удалении новости";
        public virtual string PageTitleFailedDeletingNewsCaption { get; } = "Ошибка";
        public virtual string ErrorTillDeletingNewsCaption { get; } = "Во время удаления новости произошла ошибка.Попробуйте позднее.";
        public virtual string TitleFailedUpdatingNewsCaption { get; } = "Ошибка при обновлении новости";
        public virtual string PageTitleFailedUpdatingNewsCaption { get; } = "Ошибка";
        public virtual string ErrorTillUpdatingNewsCaption { get; } = "Во время обновления новости произошла ошибка.Попробуйте позднее.";
        public virtual string PageTitleUpdatingNewsCaption { get; } = "Редактирование новости";
        public virtual string DeleteNewsQuestionCaption { get; } = "Удалить новость?";
        public virtual string NoCaption { get; } = "Нет";
        public virtual string YesCaption { get; } = "Да";
        public virtual string DeleteNewsButtonCaption { get; } = "Удалить новость";
        public virtual string SaveButtonCaption { get; } = "Сохранить";
        public virtual string GuestCaption { get; } = "Гость";
        public virtual string ProfileCaption { get; } = "Профиль";
        public virtual string ExitCaption { get; } = "Выход";
        public virtual string PersonalAccountMenuCaption { get; } = "Личный кабинет";
        public virtual string NewsFeedMenuCaption { get; } = "Новости";
        public virtual string TimesheetMenuCaption { get; } = "Таймшит";
        public virtual string TitlePersonalAccountCaption { get; } = "Профиль";
        public virtual string PageTitlePersonalAccountCaption { get; } = "Личный кабинет";
        public virtual string EmployeeNameCaption { get; } = "Имя";
        public virtual string EmployeeSurnameCaption { get; } = "Фамилия";
        public virtual string EmployeeBirthdayCaption { get; } = "Дата рождения";
        public virtual string EmployeeEmailCaption { get; } = "E-mail";
        public virtual string EmployeeOfficeCaption { get; } = "Адрес работы";
        public virtual string EmployeePositionCaption { get; } = "Должность";
        public virtual string EmployeePhoneCaption { get; } = "Телефон";
        public virtual string EmployeeAboutCaption { get; } = "О себе";
        public virtual string EmployeeEmploymentDateCaption { get; } = "Дата начала работы";
		public virtual string PAProfileCaption { get; } = "Профиль";
		public virtual string PAMineNewsCaption { get; } = "Мои новости";
		public virtual string PAModerationNewsCaption { get; } = "На модерации";
		public virtual string PAArchivedNewsCaption { get; } = "Архив";
		public virtual string PALikedNewsCaption { get; } = "Понравившееся";
		public virtual string TitleTimesheetCaption { get; } = "Таймшит";
		public virtual string PageTitleTimesheetCaption { get; } = "Таймшит";
        public virtual string MondayShortCaption { get; } = "ПН";
        public virtual string TuesdayShortCaption { get; } = "ВТ";
        public virtual string WednesdayShortCaption { get; } = "СР";
        public virtual string ThursdayShortCaption { get; } = "ЧТ";
        public virtual string FridayShortCaption { get; } = "ПТ";
        public virtual string SaturdayShortCaption { get; } = "СБ";
        public virtual string SundayShortCaption { get; } = "ВС";
        public virtual string ProjectCaption { get; } = "Проект";
        public virtual string OpenProjectsCaption { get; } = "Выбрать проект";
        public virtual string NameProjectCaption { get; } = "Название проекта";
        public virtual string CodeProjectCaption { get; } = "Код проекта";
        public virtual string TotalCaption { get; } = "Итого";
        public virtual string ActionsCaption { get; } = "Действия";
        public virtual string HoursCaption { get; } = "ч";
        public virtual string JanShortCaption { get; } = "Янв.";
        public virtual string FebShortCaption { get; } = "Фев.";
        public virtual string MarchShortCaption { get; } = "Март";
        public virtual string AprShortCaption { get; } = "Апр.";
        public virtual string MayShortCaption { get; } = "Май";
        public virtual string JuneShortCaption { get; } = "Июнь";
        public virtual string JuleShortCaption { get; } = "Июль";
        public virtual string AugShortCaption { get; } = "Авг.";
        public virtual string SepShortCaption { get; } = "Сен.";
        public virtual string OctShortCaption { get; } = "Окт.";
        public virtual string NovShortCaption { get; } = "Ноя.";
        public virtual string DecShortCaption { get; } = "Дек.";
        public virtual string DeleteCaption { get; } = "Удалить";
        public virtual string EnterCaption { get; } = "Войти";
        public virtual string TextToSupportCaption { get; } = "Обратиться в поддержку";
        public virtual string EnterProblemCaption { get; } = "Проблемы со входом?";
        public virtual string LoginCaption { get; } = "Логин";
        public virtual string PasswordCaption { get; } = "Пароль";
        public virtual string AuthCaption { get; } = "Авторизация";
        public virtual string PrivacyCaption { get; } = "Конфиденциальность";
        public virtual string BadAuthDataCaption { get; } = "Неправильные учетные данные!";
        public virtual string AuthServiceIsNotAllowedCaption { get; } = "Сервис авторизации недоступен, приносим свои извинения.";
        public virtual string RegisterCaption { get; } = "Регистрация";
        public virtual string RegisterRequestCaption { get; } = "Заявка на регистрацию";
        public virtual string SendCaption { get; } = "Отправить";
        public virtual string RegisterNewCaption { get; } = "Зарегистрироваться";
        public virtual string ChangePasswordCaption { get; } = "Сменить пароль";
        public virtual string RequestCaption { get; } = "Заявка";
        public virtual string ProblemRequestCaption { get; } = "Опишите проблему";
        public virtual string TitleChangePasswordCaption { get; } = "Смена пароля";
        public virtual string RegisterRequestSuccessCaption { get; } = "Заявка на регистрацию успешно отправлена.";
        public virtual string RegisterRequestSuccessMessageCaption { get; } = "Уведомление о регистрации придет на указанную почту.";
        public virtual string MinimumSymbolsPasswordCaption { get; } = "Пароль должен содержать как минимум 6 символов";
        public virtual string GoToEmailBoxCaption { get; } = "Для сброса пароля перейдите по ссылке в письме, отправленном на ваш email.";
        public virtual string PasswordWasResetCaption { get; } = "Ваш пароль сброшен.";
        public virtual string PasswordResetCaption { get; } = "Для входа в приложение перейдите по ";
        public virtual string LinkCaption { get; } = "ссылке";
        public virtual string ProblemRequestSucceededCaption { get; } = "Ваша заявка принята";
        public virtual string WeAnswerLaterByEmailCaption { get; } = "Ответ будет направлен Вам на указанную почту в ближайшее время.";
        public virtual string BackCaption { get; } = "Назад";
        public virtual string AccessIsRestrictedCaption { get; } = "Доступ ограничен";
        public virtual string PicturesCaption { get; } = "Фото";
        public virtual string TimeCountByProjectCaption { get; } = "Затрачено часов";
        public virtual string CreateProjectCaption { get; } = "Создать проект";
        public virtual string GoToProjects { get; } = "Перейти к проектам";
        public virtual string ModeartionMenuCaption { get; } = "Модерация";
        public virtual string PublishButtonCaption { get; } = "Опубликовать";
    }

    public class CaptionsEN : CaptionsBase
    {
        public override string CorporationSocialSystem { get; } = "Corporation social system";
        public override string TitleHomeCaption { get; } = "Main";
        public override string PageTitleHomeCaption { get; } = "Corporation social system";
        public override string GoToNewsCaption { get; } = "Go to news";
        public override string TitleNewsCaption { get; } = "News";
        public override string PageTitleNewsCaption { get; } = "Company news";
        public override string PageTitleSearchNewsCaption { get; } = "News search";
        public override string PageTitleCreatingNewsCaption { get; } = "News creation";
        public override string GoToCreatingNewsCaption { get; } = "Go to news creation";
        public override string SearchPlainFormTitleCaption { get; } = "Title (partially)";
        public override string SearchPlainFormAuthorNameCaption { get; } = "Author name";
        public override string SearchPlainFormAuthorSurnameCaption { get; } = "Author surname";
        public override string SearchPlainFormCreatedAtStartCaption { get; } = "Date from";
        public override string SearchPlainFormCreatedAtTillCaption { get; } = "Date till";
        public override string SearchPlainFormHashtagsCaption { get; } = "Hashtags (space separated, no hash)";
        public override string SearchButtonCaption { get; } = "Search";
        public override string MoreButtonCaption { get; } = "More";
        public override string HideButtonCaption { get; } = "Hide";
        public override string LoadMoreCaption { get; } = "Load more";
        public override string CreateNewsCaption { get; } = "Create news";
        public override string SearchNewsCaption { get; } = "Search";
        public override string WideSearchNewsCaption { get; } = "Advanced Search";
        public override string CreatingNewsTitleCaption { get; } = "Title";
        public override string CreatingNewsShortDescriptionCaption { get; } = "Short description";
        public override string CreatingNewsContentCaption { get; } = "Content";
        public override string CreatingNewsHashtagsCaption { get; } = "Hashtags (space separated, no hash)";
        public override string CreateButtonCaprion { get; } = "Create";
        public override string TitleCreatedNewsCaption { get; } = "The news was created successfully";
        public override string PageTitleCreatedNewsCaption { get; } = "The news was created";
        public override string ItWillBeShownAfterModerationCaption { get; } = "After passing moderation, it will be displayed in the news feed.";
        public override string TitleDeletedNewsCaption { get; } = "The news was deleted successfully";
        public override string PageTitleDeletedNewsCaption { get; } = "The news was deleted";
        public override string TitleUpdatedNewsCaption { get; } = "The news was updated successfully";
        public override string PageTitleUpdatedNewsCaption { get; } = "The news was updated";
        public override string TitleFailedCreatingNewsCaption { get; } = "Error creating news";
        public override string PageTitleFailedCreatingNewsCaption { get; } = "Error";
        public override string ErrorTillCreatingNewsCaption { get; } = "An error occurred while creating the news. Please try again later.";
        public override string TitleFailedDeletingNewsCaption { get; } = "Error deleting news";
        public override string PageTitleFailedDeletingNewsCaption { get; } = "Error";
        public override string ErrorTillDeletingNewsCaption { get; } = "An error occurred while deleting the news. Please try again later..";
        public override string TitleFailedUpdatingNewsCaption { get; } = "Error updating news";
        public override string PageTitleFailedUpdatingNewsCaption { get; } = "Error";
        public override string ErrorTillUpdatingNewsCaption { get; } = "An error occurred while updating the news. Please try again later.";
        public override string PageTitleUpdatingNewsCaption { get; } = "Update news";
        public override string DeleteNewsQuestionCaption { get; } = "Do you want to delete news?";
        public override string NoCaption { get; } = "No";
        public override string YesCaption { get; } = "Yes";
        public override string DeleteNewsButtonCaption { get; } = "Delete news";
        public override string SaveButtonCaption { get; } = "Save";
        public override string GuestCaption { get; } = "Guest";
        public override string ProfileCaption { get; } = "Profile";
        public override string ExitCaption { get; } = "Exit";
        public override string PersonalAccountMenuCaption { get; } = "Personal account";
        public override string NewsFeedMenuCaption { get; } = "News feed";
        public override string TimesheetMenuCaption { get; } = "Timesheet";
        public override string TitlePersonalAccountCaption { get; } = "Profile";
        public override string PageTitlePersonalAccountCaption { get; } = "Personal account";
        public override string EmployeeNameCaption { get; } = "Name";
        public override string EmployeeSurnameCaption { get; } = "Surname";
        public override string EmployeeBirthdayCaption { get; } = "Birthday";
        public override string EmployeeEmailCaption { get; } = "E-mail";
        public override string EmployeeOfficeCaption { get; } = "Office address";
        public override string EmployeePositionCaption { get; } = "Position";
        public override string EmployeePhoneCaption { get; } = "Phone";
        public override string EmployeeAboutCaption { get; } = "About";
        public override string EmployeeEmploymentDateCaption { get; } = "Employment date";
        public override string PAProfileCaption { get; } = "Profile";
        public override string PAMineNewsCaption { get; } = "My news";
        public override string PAModerationNewsCaption { get; } = "Moderation";
        public override string PAArchivedNewsCaption { get; } = "Archive";
        public override string PALikedNewsCaption { get; } = "Liked";
        public override string TitleTimesheetCaption { get; } = "Timesheet";
        public override string PageTitleTimesheetCaption { get; } = "Timesheet";
        public override string MondayShortCaption { get; } = "Mo";
        public override string TuesdayShortCaption { get; } = "Tu";
        public override string WednesdayShortCaption { get; } = "We";
        public override string ThursdayShortCaption { get; } = "Th";
        public override string FridayShortCaption { get; } = "Fr";
        public override string SaturdayShortCaption { get; } = "Sa";
        public override string SundayShortCaption { get; } = "Su";
        public override string ProjectCaption { get; } = "Project";
        public override string OpenProjectsCaption { get; } = "Select project";
        public override string NameProjectCaption { get; } = "Project name";
        public override string CodeProjectCaption { get; } = "Project code";
        public override string TotalCaption { get; } = "Total";
        public override string ActionsCaption { get; } = "Actions";
        public override string HoursCaption { get; } = "h";
        public override string JanShortCaption { get; } = "Jan.";
        public override string FebShortCaption { get; } = "Feb.";
        public override string MarchShortCaption { get; } = "Mar.";
        public override string AprShortCaption { get; } = "Apr.";
        public override string MayShortCaption { get; } = "May";
        public override string JuneShortCaption { get; } = "June";
        public override string JuleShortCaption { get; } = "Jule";
        public override string AugShortCaption { get; } = "Aug.";
        public override string SepShortCaption { get; } = "Sep.";
        public override string OctShortCaption { get; } = "Oct.";
        public override string NovShortCaption { get; } = "Nov.";
        public override string DecShortCaption { get; } = "Dec.";
        public override string DeleteCaption { get; } = "Delete";
        public override string EnterCaption { get; } = "Enter";
        public override string TextToSupportCaption { get; } = "Text to support";
        public override string EnterProblemCaption { get; } = "Do you have a problem?";
        public override string LoginCaption { get; } = "Login";
        public override string PasswordCaption { get; } = "Password";
        public override string AuthCaption { get; } = "Authorization";
        public override string PrivacyCaption { get; } = "Privacy";
        public override string BadAuthDataCaption { get; } = "Bad auth data!";
        public override string AuthServiceIsNotAllowedCaption { get; } = "Sorry, the service is not allowed now.";
        public override string RegisterCaption { get; } = "Registration";
        public override string RegisterRequestCaption { get; } = "Register request";
        public override string SendCaption { get; } = "Send";
        public override string RegisterNewCaption { get; } = "Register";
        public override string ChangePasswordCaption { get; } = "Change password";
        public override string RequestCaption { get; } = "Request";
        public override string ProblemRequestCaption { get; } = "Describe a problem";
        public override string TitleChangePasswordCaption { get; } = "Password changing";
        public override string RegisterRequestSuccessCaption { get; } = "The register request is successfully sent.";
        public override string RegisterRequestSuccessMessageCaption { get; } = "A notification about registration will be sent to your e-mail.";
        public override string MinimumSymbolsPasswordCaption { get; } = "Password should contain minimum 6 symbols";
        public override string GoToEmailBoxCaption { get; } = "For password reset use a link in a e-mail message, which was sent to your e-mail address.";
        public override string PasswordWasResetCaption { get; } = "Your password is reset.";
        public override string PasswordResetCaption { get; } = "Login ";
        public override string LinkCaption { get; } = "link";
        public override string ProblemRequestSucceededCaption { get; } = "The application accepted";
        public override string WeAnswerLaterByEmailCaption { get; } = "An answer will be sent to your email soon.";
        public override string BackCaption { get; } = "Back";
        public override string AccessIsRestrictedCaption { get; } = "Access restricted";
		public override string PicturesCaption { get; } = "Photo";
		public override string TimeCountByProjectCaption { get; } = "Work time";
		public override string CreateProjectCaption { get; } = "Create project";
		public override string GoToProjects { get; } = "Go to projects";
		public override string ModeartionMenuCaption { get; } = "Moderation";
		public override string PublishButtonCaption { get; } = "Publish";
	}
}
