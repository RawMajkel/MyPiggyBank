import React, {useEffect, useState} from 'react';
import axios from 'axios';
import { appConfig } from '../../config/config';

function Register() {
    
    const [userName, setUserName] = useState([]);
    const [email, setEmail] = useState([]);
    const [password, setPassword] = useState([]);

    const [errors, setErrors] = useState([]);
    const [isSubmitted = false, setIsSubmitted] = useState([]);
    const [isFinished = false, setIsFinished] = useState([]);

    function handleChange(event) {
        let id = event.target.getAttribute('id');
        let value = event.target.value;

        if(id == 'username') {
            setUserName(value);
        }
        else if(id == 'email') {
            setEmail(value);
        }
        else if (id == 'password') {
            setPassword(value);
        }
    }

    function validateEmail(email) {
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    function handleSubmit(event) {
        event.preventDefault();
        let errors = [];

        if((password.length <= 7) || !(/[A-Z]/.test(password)) || !(/[0-9]/.test(password)) || !(/[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test(password)) || !validateEmail(email)) {
            errors.push("Hasło musi zawierać co najmniej 8 znaków, jedną wielką literę, jedną liczbę oraz jeden znak specjalny");
        }

        if(userName.length <= 3) {
            errors.push("Nazwa użytkownika musi składać się z conajmniej 4 znaków");
        }

        if(!validateEmail(email)) {
            errors.push("Podaj poprawny adres e-mail");
        }

        setErrors(errors);

        if(errors.length > 0) {
            return;
        }

        setIsSubmitted(true);

        const config = { headers: { "Content-Type": "application/json" } };
        const bodyParameters = { "Username": userName, "Email": email, "Password": password };

        axios.post(`${appConfig.apiUrl}/api/v1/account/register`, bodyParameters, config).then(response => {
            if(response.status) {
                setIsFinished(true);
                window.location.href = "/login";
            }
        }).catch(({response}) => {
            console.log(response);
        });
    }

    return (
        <div className="account section">
            <div className="container">
                <span className="account__heading heading d-block font-weight-600">Zarejestruj się</span>
                <div className="account__content crud-section">
                    <form onSubmit={ handleSubmit }>
                        <label>Nazwa użytkownika</label>
                        <div className="input-group">
                            <input id="username" className="form-control" placeholder="superuser" type="text" value={userName} onChange={handleChange} />
                        </div>
                        <label>Adres e-mail</label>
                        <div className="input-group">
                            <input id="email" className="form-control" placeholder="email@address.com" type="text" value={email} onChange={handleChange} />
                        </div>
                        <label>Hasło</label>
                        <div className="input-group">
                            <input placeholder="********" id="password" className="form-control" type="password" value={password} onChange={handleChange} />
                        </div>
                        <input className="button d-inline-flex align-items-center justify-content-start color-white" type="submit" value="Zarejestruj" />
                        <ul style={{ marginTop : '10px', color : 'red', listStyle : 'disc', listStylePosition : 'inside' }}>
                            { 
                                errors.map(function(element, index) {
                                    return <li className="error" key={index}>{element}</li>
                                })
                            }
                        </ul>
                        { isSubmitted == true && isFinished == false && <div style={{marginTop : '10px'}}>Autoryzacja - proszę czekać</div> }
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Register;