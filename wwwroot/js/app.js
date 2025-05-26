// Focus function for search input
window.focusElement = (selector) => {
    const element = document.querySelector(selector);
    if (element) {
        element.focus();
    }
};
