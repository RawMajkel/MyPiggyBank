import React from 'react';
import Operation from '../Operations/Operation';

function OperationGroup({date, operations}) {

    let today = new Date();
    let dd = String(today.getDate()).padStart(2, '0');
    let mm = String(today.getMonth() + 1).padStart(2, '0');
    let yyyy = today.getFullYear();
    
    today = `${yyyy}-${mm}-${dd}`;

    if(date == today) date = "Dzisiaj";

  return (
    <div className="latest-group">
        <div className="latest-group__date position-relative w-100 d-flex align-items-center justify-content-start">
            <span className="latest-group__datelabel w-100 text-uppercase font-weight-600">{ date }</span>
        </div>
        <div className="latest-group__list">
            {
                operations.map(function(el, index) {
                    return <Operation name={el.Name} value={el.Value} key={index} />
                })
            }
        </div>
    </div>
  );
}

export default OperationGroup;