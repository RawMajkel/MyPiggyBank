import React, {useState, useEffect } from 'react';
import axios from 'axios';
import { appConfig } from '../../config/config';

function DeleteCategory({id, token}) {

  useEffect(() => {
      (async () => {
          const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };

          await axios.delete(`${appConfig.apiUrl}/api/v1/operationcategories/${id}`, config).then(response => {
              if(response.status) {
                  window.location.href = "/categories";
              }
          }).catch(({response}) => {
              console.log(response);
          });
      })();
  }, []);

  return (
    <div className="resources section">
      <div className="container">
        <span className="heading d-block font-weight-600">Usuwam kategorię</span>
        <p>Proszę czekać</p>
      </div>
    </div>
  );
}

export default DeleteCategory;