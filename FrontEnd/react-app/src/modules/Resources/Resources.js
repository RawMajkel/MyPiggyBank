import React from 'react';
import Resource from '../Resources/Resource';
import { v4 as uuidv4 } from 'uuid';

function Resources({token}) {

  const tempData = [
    { "Id": uuidv4(), "Name": "Karta mBank", "Value": 3250.99, "Currency": "PLN" },
    { "Id": uuidv4(), "Name": "Karta Revolut", "Value": 1090.31, "Currency": "PLN" },
    { "Id": uuidv4(), "Name": "Gotówka", "Value": 300.03, "Currency": "PLN" }
  ];

  // return data
  // public Guid Id { get; set; }
  // public Guid UserId { get; set; }
  // public string Name { get; set; }
  // public decimal Value { get; set; }
  // public string Currency { get; set; }

  return (
    <div className="resources section">
      <div className="container">
          <span className="resources___heading heading d-block font-weight-600">Lista rachunków:</span>
          <div className="resources__list">
            <div>
                { tempData.map(function(el, index) { return <Resource id={el.Id} name={el.Name} value={el.Value} currency={el.Currency} key={index} /> }) }
            </div>
          </div>
          <a href="/resources/add" className="button d-inline-flex align-items-center justify-content-start">
              <i className="fas fa-plus button__icon color-white"></i>
              <span className="color-white">Dodaj rachunek</span>
          </a>  
      </div>
  </div>
  );
}

export default Resources;