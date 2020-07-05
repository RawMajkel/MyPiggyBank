import React from 'react';
import CyclicOperation from '../CyclicOperations/CyclicOperation';

function CyclicGroup({operations, period}) {

    let periodName;

    if(period == 7) periodName = "cotygodniowe"; 
    if(period == 31) periodName = "comiesięczne"; 
    if(period == 365) periodName = "coroczne"; 

    return (
        <div className="latest-group">
            <div className="latest-group__date position-relative w-100 d-flex align-items-center justify-content-start">
                <span className="latest-group__datelabel w-100 text-uppercase font-weight-600">{ periodName }</span>
            </div>
            <div className="latest-group__list">
                {
                    operations.map(function(el, index) {
                        return <CyclicOperation id={el.Id} resourceId={el.resourceId} categoryId={el.categoryId} name={el.Name} value={el.EstimatedValue} key={index} />
                    })
                }
            </div>
        </div>
    );
}

export default CyclicGroup;