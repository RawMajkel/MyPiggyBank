import React from 'react';

function Header({label, identifier, data}) {
  return (
    <div className="header header--index">
        <nav className="navigation position-fixed w-100">
            <div className="container">
                <div className="navigation__row d-flex align-items-center justify-content-start">
                    <button className="navigation__trigger d-flex align-items-center justify-content-start" style={{border : 'none', backgroundColor : 'transparent'}}>
                        <i className="navigation__bars color-second fas fa-bars"></i>
                    </button>
                    <aside className="navigation__side-menu w-100 position-absolute">
                        <div className="navigation__name text-uppercase">{ identifier }</div>
                        <hr className="navigation__hr" />
                        <ul className="navigation__ul">
                            {
                                data.map(function(element, index) {
                                return <li className="navigation__li" key={index}>
                                    <a href={ element.url } className="navigation__a line-2 d-block">
                                    <i className={ 'navigation__icon fas ' + element.icon }></i>
                                    <span className="navigation__span">{ element.name }</span>
                                    </a>
                                </li>
                                })
                            }
                        </ul>
                        <div className="navigation__copy">
                            <span className="navigation__copy-label d-block">&copy; 2020 Wszystkie prawa zastrzeżone</span>
                            <span className="navigation__copy-label d-block">MyPiggyBank v.1.0</span>
                        </div>
                    </aside>
                    <span className="navigation__label d-block">{ label }</span>
                </div>
            </div>
        </nav>
    </div>
  );
}

export default Header;