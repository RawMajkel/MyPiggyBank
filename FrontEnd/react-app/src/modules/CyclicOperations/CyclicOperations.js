import React, {useState, useEffect } from 'react';
import axios from 'axios';
import CyclicGroup from '../CyclicOperations/CyclicGroup';
import { appConfig } from '../../config/config';

/* sort operations by period */
function divideOperations(data) {
  data.sort((a,b) => (a.period > b.period) ? 1 : ((b.period > a.period) ? -1 : 0)); 
  data.reverse();

  data.forEach(function(el, index) {
    if(!el.isIncome) el.estimatedValue *= -1;
  });

  return data.reduce((res, curr) => {
    if (res[curr.period]) res[curr.period].push(curr);
    else Object.assign(res, {[curr.period]: [curr]});

    return res;
  }, {});
}

function CyclicOperations({token}) {

  const [cyclicOperations, setCyclicOperations] = useState([]);

  useEffect(() => {
      (async () => {
          const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };
          const bodyParameters = { "Name": null };

          const response = await axios.post(`${appConfig.apiUrl}/api/v1/cyclicoperations/list`, bodyParameters, config);
          setCyclicOperations(response.data);
      })();
  }, []);

  const groups = divideOperations(cyclicOperations);

  /* create period groups */
  let groupsArray = [];

  for (var key in groups) {
    if (!groups.hasOwnProperty(key)) continue;
    var obj = groups[key];
    
    groupsArray.push(obj);

    for (var prop in obj) {
        if (!obj.hasOwnProperty(prop)) continue;
    }
  }

  return (
    <div className="latest section">
        <div className="container">
            <span className="latest__heading heading d-block font-weight-600">Operacje cykliczne:</span>
            {
              groupsArray.map(function(el, index) { return <CyclicGroup operations={el} period={el[0].period} key={index} /> })
            }
            <a href="/cyclic/add" className="button d-inline-flex align-items-center justify-content-start">
                <i className="fas fa-plus button__icon color-white"></i>
                <span className="color-white">Dodaj operację cykliczną</span>
            </a>
        </div>
    </div>
  );
}

export default CyclicOperations;