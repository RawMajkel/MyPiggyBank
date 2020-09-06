import React from 'react';

function Resource({id, name, value, currency}) {
    return (
        <div className="resources__item d-flex align-items-center justify-content-between">
            <div className="resources__content d-flex align-items-center justify-content-start">
                <i className="fas fa-credit-card resources__img"></i>
                <div className="resources__desc">
                    <div className="resources__title">{ name }</div>
                    <span className="resources__value font-weight-600 d-block">{value} {currency}</span>
                </div>
            </div>
            <div className="resource__close">
                <a href={ `/resources/delete/${id}` } className="resources__label d-flex align-items-center justify-content-start color-second">
                    Usuń 
                    <i className="fas fa-times-circle resources__icon"></i>
                </a>
            </div>
        </div>
    );
}

export default Resource;