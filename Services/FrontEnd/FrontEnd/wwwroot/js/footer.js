document.addEventListener('mousemove', function (event) {
    const footer = document.querySelector('.footer');
    const viewportHeight = window.innerHeight;
    const cursorY = event.clientY;

    if (cursorY > viewportHeight - 100) {
        footer.classList.add('visible');
    } else {
        footer.classList.remove('visible');
    }
});
