import React, {useState, useEffect } from 'react';
import axios from 'axios';
import Category from '../Categories/Category';
import { appConfig } from '../../config/config';

function Categories({token}) {

    const [categories, setCategories] = useState([]);

    useEffect(() => {
        (async () => {
            const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };
            const bodyParameters = { "Name": null };

            const response = await axios.post(`${appConfig.apiUrl}/api/v1/operationcategories/list`, bodyParameters, config);
            setCategories(response.data);
        })();
    }, []);

  return (
    <div className="resources section">
        <div className="container">
            <span className="resources___heading heading d-block font-weight-600">Lista kategorii:</span>
            <div className="resources__list">
                <div>
                    { categories.map(function(el, index) { return <Category id={el.id} name={el.name} key={index} /> }) }
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