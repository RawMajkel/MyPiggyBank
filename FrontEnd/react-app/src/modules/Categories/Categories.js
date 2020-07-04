import React from 'react';
import Category from '../Categories/Category';
import { v4 as uuidv4 } from 'uuid';

function Categories({token}) {

    const tempData = [
        { "Id": uuidv4(), "Name": "Zdrowie" },
        { "Id": uuidv4(), "Name": "Jedzenie" },
        { "Id": uuidv4(), "Name": "Samochód" },
        { "Id": uuidv4(), "Name": "Rozrywka" },
        { "Id": uuidv4(), "Name": "Edukacja" },
        { "Id": uuidv4(), "Name": "Inne" }
    ];

  return (
    <div className="resources section">
        <div className="container">
            <span className="resources___heading heading d-block font-weight-600">Lista kategorii:</span>
            <div className="resources__list">
                <div>
                    { tempData.map(function(el, index) { return <Category id={el.Id} name={el.Name} key={index} /> }) }
                </div>
            </div>
            <a href="/categories/add" className="button d-inline-flex align-items-center justify-content-start">
                <i className="fas fa-plus button__icon color-white"></i>
                <span className="color-white">Dodaj kategorię</span>
            </a>  
        </div>
    </div>
  );
}

export default Categories;