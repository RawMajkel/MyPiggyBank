import React, {useState} from 'react';
import { v4 as uuidv4 } from 'uuid';
import axios from 'axios';

function AddResource({token}) {

    const [name, setName] = useState([]);
    const [value, setValue] = useState([]);
  
    const [errors, setErrors] = useState([]);
    const [isSubmitted, setIsSubmitted] = useState([]);
    const [isFinished = false, setIsFinished] = useState([]);

    function handleChange(event) {
        let id = event.target.getAttribute('id');
        let value = event.target.value;
    
        if(id == 'name') {
          setName(value);
        }
        else if (id == 'value') {
          setValue(value);
        }
      }
    
      function handleSubmit(event) {
        event.preventDefault();
    
        let errors = [];
    
        if(name.length < 2) { errors.push("Podaj poprawną nazwę zasobu"); }
        setErrors(errors);
    
        if(errors.length > 0) return;
        setIsSubmitted(true);

        const config = { headers: { "Content-Type": "application/json", "Authorization": `Bearer ${token}` } };
        const bodyParameters = { Id: uuidv4(), Name: name, Value: parseFloat(value), Currency: "PLN" };

        axios.post('https://localhost:5001/api/v1/resources/save', bodyParameters, config).then(response => {
            if(response.status) {
                setIsFinished(true);
                window.location.href = "/resources";
            }
        }).catch(({response}) => {
            console.log(response);
        });
      }

    return (
        <div className="resources section">
            <div className="container">
                <span className="account__heading heading d-block font-weight-600">Dodaj rachunek</span>
                <div className="account__content crud-section">
                    <form onSubmit={ handleSubmit }>
                        <label>Nazwa</label>
                        <div className="input-group">
                            <input id="name" className="form-control" placeholder="Karta mBank" type="text" value={name} onChange={handleChange} />
                        </div>
                        <label>Wartość</label>
                        <div className="input-group">
                            <input id="value" className="form-control" placeholder="1999.99" type="text" value={value} onChange={handleChange} />
                        </div>
                        <label>Waluta</label>
                        <div className="input-group">
                            <select className="custom-select" onChange={handleChange}>
                                <option>PLN</option>
                            </select>
                        </div>
                        <input className="button d-inline-flex align-items-center justify-content-start color-white" type="submit" value="Dodaj rachunek" />
                        <ul style={{ marginTop : '10px', color : 'red', listStyle : 'disc', listStylePosition : 'inside' }}>
                            { 
                                errors.map(function(element, index) {
                                    return <li className="error" key={index}>{element}</li>
                                })
                            }
                        </ul>
                    </form>
                </div>
            </div>
        </div>
      );
}

export default AddResource;