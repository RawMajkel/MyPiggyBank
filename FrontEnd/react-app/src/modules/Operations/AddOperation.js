import React, {useState, useEffect} from 'react';
import { v4 as uuidv4 } from 'uuid';
import axios from 'axios';

function AddOperation({token}) {
  
  /* form */
  const [resourceId, setResourceId] = useState([]);
  const [operationCategoryId, setOperationCategoryId] = useState([]);
  const [name, setName] = useState([]);
  const [description, setDescription] = useState([]);
  const [isIncome, setIsIncome] = useState([]);
  const [value, setValue] = useState([]);
  const [date, setDate] = useState([]);

  /* state */
  const [errors, setErrors] = useState([]);
  const [isSubmitted, setIsSubmitted] = useState([]);
  const [isFinished = false, setIsFinished] = useState([]);

  /* others */
  const [categories, setCategories] = useState([]);
  const [resources, setResources] = useState([]);

  useEffect(() => {
    (async () => {
      const config = { headers: { "Content-Type": "application/json; charset=utf-8", "Authorization": `Bearer ${token}` } };
      const bodyParameters = { "Name": null };

      const responseCategories = await axios.post('https://localhost:5001/api/v1/operationcategories/list', bodyParameters, config);
      const responseResources = await axios.post('https://localhost:5001/api/v1/resources/list', bodyParameters, config);

      setCategories(responseCategories.data);
      setResources(responseResources.data);
    })();
  }, []);

 function handleChange(event) {
  let id = event.target.getAttribute('id');
  let value = event.target.value;

  if(id == 'resource') { setResourceId(event.target.options[event.target.selectedIndex].id); }
  else if(id == 'category') { setOperationCategoryId(event.target.options[event.target.selectedIndex].id); }
  else if(id == 'name') { setName(value); }
  else if(id == 'description') { setDescription(value); }
  else if(id == 'isIncome') { setIsIncome(event.target.options[event.target.selectedIndex].value); }
  else if(id == 'value') { setValue(value); }
  else if(id == 'date') { setDate(value); }
}

function handleSubmit(event) {
  event.preventDefault();

  let errors = [];

  if(name.length < 2) { errors.push("Podaj poprawną nazwę operacji"); }

  setErrors(errors);

  if(errors.length > 0) return;

  setIsSubmitted(true);

  const config = { headers: { "Content-Type": "application/json", "Authorization": `Bearer ${token}` } };
  const bodyParameters = { "Id": uuidv4(), "ResourceId": resourceId, "OperationCategoryId": operationCategoryId, "Name": name, "Description": description, "IsIncome": isIncome == "true" ? true : false, "Value": parseFloat(value), "OccuredAt": date };

  axios.post('https://localhost:5001/api/v1/operations/save', bodyParameters, config).then(response => {
      if(response.status) {
          setIsFinished(true);
          window.location.href = "/operations";
      }
  }).catch(({response}) => {
      console.log(response);
  });
}

  return (
    <div className="operations section">
      <div className="container">
          <span className="operations__heading heading d-block font-weight-600">Dodaj operację</span>
          <div className="account__content crud-section">
                <form onSubmit={ handleSubmit }>
                    <label>Nazwa</label>
                    <div className="input-group">
                        <input id="name" className="form-control" placeholder="Nazwa transakcji" type="text" value={name} onChange={handleChange} />
                    </div>
                    <label>Wartość</label>
                    <div className="input-group">
                        <input id="value" className="form-control" placeholder="Wpisz wartość (PLN)" type="number" value={value} onChange={handleChange} />
                    </div>
                    <label>Opis</label>
                    <div className="input-group">
                        <input id="description" className="form-control" placeholder="Opis transakcji" type="text" value={description} onChange={handleChange} />
                    </div>
                    <label>Rodzaj operacji</label>
                    <div className="input-group">
                        <select id="isIncome" className="custom-select" onChange={handleChange}>
                          <option value="null" disabled selected>- Wybierz -</option>
                          <option value="true">Przychód</option>
                          <option value="false">Wydatek</option>
                        </select>
                    </div>
                    <label>Rachunek</label>
                    <div className="input-group">
                        <select id="resource" className="custom-select" onChange={handleChange}>
                          <option value="null" disabled selected>- Wybierz -</option>
                          { 
                              resources.map(function(element, index) {
                                  return <option id={element.id} key={index}>{element.name}</option>
                              })
                          }
                        </select>
                    </div>
                    <label>Kategoria</label>
                    <div className="input-group">
                        <select id="category" className="custom-select" onChange={handleChange}>
                          <option value="null" disabled selected>- Wybierz -</option>
                          { 
                              categories.map(function(element, index) {
                                  return <option id={element.id} key={index}>{element.name}</option>
                              })
                          }
                        </select>
                    </div>
                    <label>Data operacji</label>
                    <div className="input-group">
                        <input id="date" className="form-control" placeholder="Opis wydatku" type="date" value={date} onChange={handleChange} />
                    </div>
                    <input className="button d-inline-flex align-items-center justify-content-start color-white" type="submit" value="Dodaj operację" />
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

export default AddOperation;