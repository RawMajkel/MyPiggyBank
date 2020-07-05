import React from 'react';

function Category({id, name}) {
  return (
    <div className="resources__item d-flex align-items-center justify-content-between">
        <div className="resources__content d-flex align-items-center justify-content-start">
            <i className="fas fa-bookmark resources__img"></i>
            <div className="resources__desc">
                <div className="resources__title">{ name }</div>
            </div>
        </div>
        <div className="resource__close">
            <a href={ `/categories/delete/${id}` } className="resources__label d-flex align-items-center justify-content-start color-second">
                Usuń 
                <i className="fas fa-times-circle resources__icon"></i>
            </a>
        </div>
    </div>
  );
}

export default Category;