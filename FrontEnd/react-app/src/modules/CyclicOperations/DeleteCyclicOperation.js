import React, {useEffect } from 'react';
import axios from 'axios';
import { appConfig } from '../../config/config';

function DeleteCyclicOperation({token, id}) {

  useEffect(() => {
    (async () => {
        const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };

        const response = await axios.delete(`${appConfig.apiUrl}/api/v1/cyclicoperations/${id}`, config).then(response => {
            if(response.status) {
                window.location.href = "/cyclic";
            }
        }).catch(({response}) => {
            console.log(response);
        });
    })();
}, []);

return (
  <div className="resources section">
    <div className="container">
      <span className="heading d-block font-weight-600">Usuwam operację cykliczną</span>
      <p>Proszę czekać</p>
    </div>
  </div>
);
}

export default DeleteCyclicOperation;