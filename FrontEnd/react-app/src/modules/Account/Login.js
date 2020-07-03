import React, {useState} from 'react';
import Authenticate from '../Global/Authenticate';

function validateEmail(email) {
  const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(String(email).toLowerCase());
}

function Login() {

  const [email, setEmail] = useState([]);
  const [password, setPassword] = useState([]);

  const [isSubmitted, setIsSubmitted] = useState([]);
  const [errors, setErrors] = useState([]);

  function handleChange(event) {
    let id = event.target.getAttribute('id');
    let value = event.target.value;

    if(id == 'email') {
      setEmail(value);
    }
    else if (id == 'password') {
      setPassword(value);
    }
  }

  function handleSubmit(event) {
    event.preventDefault();

    let errors = [];

    if(password.length <= 7 || !(/[A-Z]/.test(password)) || !(/[1-9]/.test(password)) || (/^[a-zA-Z0-9!@#\$%\^\&*\)\(+=._-]+$/g.test(password))) {
        errors.push("Hasło musi zawierać co najmniej 8 znaków, jedną wielką literę, jedną liczbę oraz jeden znak specjalny");
    }

    if(!validateEmail(email)) {
        errors.push("Podaj poprawny adres e-mail");
    }

    setErrors(errors);

    if(errors.length > 0) {
        return;
    }

    setIsSubmitted(true);
  }

  return (
    <div className="account section">
        <div className="container">
            <span className="account__heading heading d-block font-weight-600">Zaloguj się</span>
            <div className="account__content crud-section">
                <form onSubmit={ handleSubmit }>
                    <label>Adres e-mail</label>
                    <div className="input-group">
                        <input id="email" className="form-control" placeholder="email@address.com" type="text" value={email} onChange={handleChange} />
                    </div>
                    <label>Hasło</label>
                    <div className="input-group">
                        <input placeholder="********" id="password" className="form-control" type="password" value={password} onChange={handleChange} />
                    </div>
                    <input className="button d-inline-flex align-items-center justify-content-start color-white" type="submit" value="Zaloguj" />
                    <ul style={{ marginTop : '10px', color : 'red', listStyle : 'disc', listStylePosition : 'inside' }}>
                        { 
                            errors.map(function(element, index) {
                                return <li className="error" key={index}>{element}</li>
                            })
                        }
                    </ul>
                    { isSubmitted == true && <Authenticate email={email} password={password} /> }
                </form>
            </div>
        </div>
    </div>
  );
}

export default Login;