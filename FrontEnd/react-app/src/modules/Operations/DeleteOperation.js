import React, {useEffect } from 'react';
import axios from 'axios';

function DeleteOperation({token, id}) {

  useEffect(() => {
    (async () => {
        const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };

        await axios.delete(`https://localhost:5001/api/v1/operations/${id}`, config).then(response => {
            if(response.status) {
                window.location.href = "/operations";
            }
        }).catch(({response}) => {
            console.log(response);
        });
    })();
}, []);

return (
  <div className="resources section">
    <div className="container">
      <span className="heading d-block font-weight-600">Usuwam operację</span>
      <p>Proszę czekać</p>
    </div>
  </div>
);
}

export default DeleteOperation;