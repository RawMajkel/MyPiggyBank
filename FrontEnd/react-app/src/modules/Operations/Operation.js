import React from 'react';

function Operation({name, value}) {

  return (
    <div className="latest-item d-flex align-items-center flex-nowrap justify-content-start">
        <i className={ `latest-item__icon fas ${value > 0 ? 'fa-share color-green' : 'fa-reply color-red'}` }></i>
        <div className="latest-item__desc d-flex w-100 justify-content-between align-items-center">
            <div className="latest-item__title line-1 text-uppercase">{ name }</div>
            <span className="latest-item__value font-weight-500 w-100 text-right">{value} zł</span>
        </div>
    </div>
  );
}

export default Operation;