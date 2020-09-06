import React, {useState, useEffect } from 'react';
import axios from 'axios';
import OperationGroup from '../Operations/OperationGroup';
import { appConfig } from '../../config/config';

function divideOperations(data) {
  data.sort((a,b) => (a.occuredAt > b.occuredAt) ? 1 : ((b.occuredAt > a.occuredAt) ? -1 : 0)); 
  data.reverse();

  data.forEach(function(el, index) {
    if(!el.isIncome) el.value *= -1;
  });

  return data.reduce((res, curr) => {
    if (res[curr.occuredAt]) res[curr.occuredAt].push(curr);
    else Object.assign(res, {[curr.occuredAt]: [curr]});

    return res;
  }, {});
}

function Operations({token, limit}) {

  const [operations, setOperations] = useState([]);

  useEffect(() => {
      (async () => {
          const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };
          const bodyParameters = limit ? { "Name": null, "Limit": limit } : { "Name": null };

          const response = await axios.post(`${appConfig.apiUrl}/api/v1/operations/list`, bodyParameters, config);
          setOperations(response.data);
      })();
  }, []);

  const groups = divideOperations(operations);

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
    <div>
        { groupsArray.map(function(el, index) { return <OperationGroup date={el[0].occuredAt} operations={el} key={index} /> }) }
    </div>
  );
}

export default Operations;