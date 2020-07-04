import React from 'react';
import OperationGroup from '../Operations/OperationGroup';

function divideOperations(data) {
  data.sort((a,b) => (a.OccuredAt > b.OccuredAt) ? 1 : ((b.OccuredAt > a.OccuredAt) ? -1 : 0)); 
  data.reverse();

  data.forEach(function(el, index) {
    if(!el.IsIncome) el.Value *= -1;
  });

  return data.reduce((res, curr) => {
    if (res[curr.OccuredAt]) res[curr.OccuredAt].push(curr);
    else Object.assign(res, {[curr.OccuredAt]: [curr]});

    return res;
  }, {});
}

function Operations({token, limit}) {

  const tempData = [
    { "Name": "Zakupy w Tesco", "Value": 390.99, "OccuredAt": '2020-07-03', "IsIncome": false },
    { "Name": "Zakupy", "Value": 90.31, "OccuredAt": '2020-07-03', "IsIncome": false },
    { "Name": "SPRZEDAŻ HANTLI NA SIŁOWNIĘ", "Value": 250.00, "OccuredAt": '2020-02-01', "IsIncome": true },
    { "Name": "Biedronka - zakupy", "Value": 90.31, "OccuredAt": '2020-02-01', "IsIncome": false },
    { "Name": "Tankowanie samochodu", "Value": 190.1, "OccuredAt": '2020-02-01', "IsIncome": false },
    { "Name": "Naprawa kosiarki", "Value": 90.31, "OccuredAt": '2020-06-01', "IsIncome": false },
    { "Name": "Gra komputerowa", "Value": 90.31, "OccuredAt": '2020-06-01', "IsIncome": false },
    { "Name": "Zakupy", "Value": 90.31, "OccuredAt": '2020-05-01', "IsIncome": false },
    { "Name": "Skrzynka z narzędziami", "Value": 90.31, "OccuredAt": '2020-01-08', "IsIncome": false },
    { "Name": "Spotify", "Value": 19.99, "OccuredAt": '2020-01-08', "IsIncome": false },
    { "Name": "Mechanik samochodowy", "Value": 200.00, "OccuredAt": '2020-01-08', "IsIncome": false }
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
        <div>
            { groupsArray.map(function(el, index) { return <OperationGroup date={el[0].OccuredAt} operations={el} key={index} /> }) }
        </div>
    );
}

export default Operations;