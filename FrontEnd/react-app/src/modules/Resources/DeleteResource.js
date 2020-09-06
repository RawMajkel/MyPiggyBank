import React, {useState, useEffect } from 'react';
import axios from 'axios';

function DeleteResource({id, token}) {
    
  useEffect(() => {
    (async () => {
        const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };

        const response = await axios.delete(`https://localhost:5001/api/v1/resources/${id}`, config).then(response => {
            if(response.status) {
                window.location.href = "/resources";
            }
        }).catch(({response}) => {
            console.log(response);
        });
    })();
}, []);

  return (
    <div className="resources section">
      <div className="container">
        <span className="heading d-block font-weight-600">Usuwam rachunek</span>
        <p>Proszę czekać</p>
      </div>
    </div>
  );
}

export default DeleteResource;