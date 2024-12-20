document.addEventListener('DOMContentLoaded', function () {
    const filters = document.querySelectorAll('.filter');
    const cards = document.querySelectorAll('.cards li');
    filters.forEach(filter => {
        filter.addEventListener('click', function () {
            const category = this.getAttribute('data-category');

            cards.forEach(card => {
                const cardCategory = card.getAttribute('data-category');

                if (category === 'all' || category === cardCategory) {
                    card.classList.remove('hidden');
                } else {
                    card.classList.add('hidden');
                }
            });
        });
    });
});
function toggleActive(element) {
    var buttons = document.querySelectorAll('.filter button');
    buttons.forEach(btn => btn.classList.remove('active'));

    element.classList.add('active');

    var category = element.parentNode.dataset.category;

    var cards = document.querySelectorAll('.card11 .container11');
    cards.forEach(card => {
        if (category === 'all' || card.classList.contains(category)) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });

    return false;
}
/**  Login **/
function toggleForms(formId) {
    const loginContainer = document.getElementById('loginContainer');
    const signupContainer = document.getElementById('signupContainer');

    if (formId === 'signupForm') {
      signupContainer.style.display = 'block';
      loginContainer.style.display = 'none';
    } else {
      loginContainer.style.display = 'block';
      signupContainer.style.display = 'none';
    }
}
function loginUser() {
    window.location.href = '../index.html';
    return false; 
 }
/*---------------header-------------------*/
var isMenuOpen = false;
function toggleMenu() {
    var mainMenu = document.getElementById('mainMenu');
    mainMenu.classList.toggle('active');
}
function clearSearchInput() {
    var searchInput = document.getElementById('searchInput');
    searchInput.value = '';

    var mainMenu = document.getElementById('mainMenu');
    mainMenu.style.display = 'none';
}
function scrollToElement(elementId, event) {
    var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth' });

        if (event && event.target.tagName !== 'A') {
            var mainMenu = document.getElementById('mainMenu');
            setTimeout(function () {
                mainMenu.style.display = 'none';
            }, 100); 
        }
    }
}
/*  ------------- Search ----------*/
function clearSearchInput() {
    var searchInput = document.getElementById('searchInput');
    searchInput.value = '';
}
/*****************       */

  function toggleDarkMode() {
    const body = document.body;
    const isDarkMode = body.classList.contains('dark-mode');

    body.classList.toggle('dark-mode', !isDarkMode);

    localStorage.setItem('darkMode', !isDarkMode);
}

const savedDarkMode = localStorage.getItem('darkMode');
if (savedDarkMode === 'true') {
    document.body.classList.add('dark-mode');
}
/*------------------------*/
document.addEventListener('DOMContentLoaded', function () {
    var containerElements = document.querySelectorAll('.container11');
    
    containerElements.forEach(function(containerElement) {
        containerElement.addEventListener('click', function () {
            var videoPath = containerElement.dataset.videoPath;
            window.location.href = videoPath;
        });
    });
});