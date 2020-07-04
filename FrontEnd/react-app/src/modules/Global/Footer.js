import React from 'react';

const footerElements = [
  { name: 'Pulpit', url: '/', icon: 'fa-home' },
  { name: 'Rachunki', url: '/resources', icon: 'fa-credit-card' },
  { name: 'Cykliczne', url: '/cyclic', icon: 'fa-undo-alt' },
  { name: 'Operacje', url: '/operations', icon: 'fa-exchange-alt' }
];

function Footer() {
  return (
    <nav className="footernav position-fixed w-100">
        <div className="container">
            <ul className="footernav__ul d-flex flex-nowrap align-items-center justify-content-around">
              {
                footerElements.map(function(element, index) {
                  return <li className="footernav__item" key={index}>
                    <a href={ element.url } className={ 'footernav__a d-flex flex-column align-items-center' + (element.url == window.location.pathname ? ' footernav__a--active' : '') }>
                      <i className={ 'footernav__icon fas ' + element.icon }></i>
                      <span className="footernav__label">{ element.name }</span>
                    </a>
                  </li>
                })
              }
            </ul>
        </div>
    </nav>
  );
}

export default Footer;