import React from 'react';
import CyclicGroup from '../CyclicOperations/CyclicGroup';
import { v4 as uuidv4 } from 'uuid';

function divideOperations(data) {
  data.sort((a,b) => (a.Period > b.Period) ? 1 : ((b.Period > a.Period) ? -1 : 0)); 
  data.reverse();

  data.forEach(function(el, index) {
    if(!el.IsIncome) el.EstimatedValue *= -1;
  });

  return data.reduce((res, curr) => {
    if (res[curr.Period]) res[curr.Period].push(curr);
    else Object.assign(res, {[curr.Period]: [curr]});

    return res;
  }, {});
}

function CyclicOperations({token}) {

  const tempData = [
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Spotify", "IsIncome": false, "EstimatedValue": 19.99, "Period": 31 },
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Siłownia", "IsIncome": false, "EstimatedValue": 79.99, "Period": 31 },
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Samochód OC", "IsIncome": false, "EstimatedValue": 1300.00, "Period": 365 },
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Zakupy", "IsIncome": false, "EstimatedValue": 70.00, "Period": 7 },
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Ubezpieczenie", "IsIncome": false, "EstimatedValue": 821.21, "Period": 365 },
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Hostingi", "IsIncome": false, "EstimatedValue": 142.12, "Period": 365 },
    { "Id": uuidv4(), "ResourceId": uuidv4(), "OperationCategoryId": uuidv4(), "Name": "Wypłata", "IsIncome": true, "EstimatedValue": 12999.99, "Period": 31 },
  ];

  const groups = divideOperations(tempData);

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
              groupsArray.map(function(el, index) { return <CyclicGroup operations={el} period={el[0].Period} key={index} /> })
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