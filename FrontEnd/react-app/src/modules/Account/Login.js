import React, {useState} from 'react';
import Authenticate from '../Global/Authenticate';

function Login() {

  const [email, setEmail] = useState([]);
  const [password, setPassword] = useState([]);
  const [isSubmitted, setIsSubmitted] = useState([]);

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
                    { isSubmitted == true && <Authenticate email={email} password={password} /> }
                </form>
            </div>
        </div>
    </div>
  );
}

export default Login;