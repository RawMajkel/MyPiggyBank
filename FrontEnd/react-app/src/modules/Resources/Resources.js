import React, {useState, useEffect } from 'react';
import axios from 'axios';
import Resource from '../Resources/Resource';
import { appConfig } from '../../config/config';

function Resources({token}) {

  const [resources, setResources] = useState([]);

  useEffect(() => {
      (async () => {
          const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };
          const bodyParameters = { "Name": null };

          const response = await axios.post(`${appConfig.apiUrl}/api/v1/resources/list`, bodyParameters, config);
          setResources(response.data);
      })();
  }, []);

  return (
    <div className="resources section">
      <div className="container">
          <span className="resources___heading heading d-block font-weight-600">Lista rachunków:</span>
          <div className="resources__list">
            <div>
                { resources.map(function(el, index) { return <Resource id={el.id} name={el.name} value={el.value} currency={el.currency} key={index} /> }) }
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