const navHeight = document.getElementsByClassName('navigation')[0].offsetHeight;
const footerHeight = document.getElementsByClassName('footernav')[0].offsetHeight;

const header = document.getElementsByClassName('header')[0];
const body = document.getElementsByTagName('body')[0];

header.style.paddingTop = `${navHeight}px`;
body.style.marginBottom = `${footerHeight}px`;