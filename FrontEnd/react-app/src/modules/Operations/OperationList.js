import React from 'react';
import Operations from '../Operations/Operations';

function OperationList({token}) {
  return (
    <div className="latest section">
      <div className="container">
        <span className="latest__heading heading d-block font-weight-600">Transakcje</span>
        <Operations limit={10} />
          <a href="/operations/add" className="button d-inline-flex align-items-center justify-content-start">
              <i className="fas fa-plus button__icon color-white"></i>
              <span className="color-white">Nowa transakcja</span>
          </a>  
      </div>
    </div>
  );
}

export default OperationList;