import React, {useEffect, useState} from 'react';
import axios from 'axios';

function Authenticate({email, password}) {

    const [hasErrors = false, setHasErrors] = useState([]);
    
    useEffect(() => {
        (async () => {
            const config = { headers: { "Content-Type": "application/json" } };
            const bodyParameters = { "Email": email, "Password": password };

            await axios.post('https://localhost:5001/api/v1/account/login', bodyParameters, config).then(response => {
                if(response.status) {

                    let expires = "";
                    let days = 7;
                    
                    if (days) {
                        var date = new Date();
                        date.setTime(date.getTime() + (days*24*60*60*1000));
                        expires = "; expires=" + date.toUTCString();
                    }
                    document.cookie = "PiggyBank.UserToken=" + response.data.token  + expires + "; path=/";
                    document.cookie = "PiggyBank.Identifier=" + response.data.identifier  + expires + "; path=/";
            
                    window.location.href = "/";
                }
            }).catch(({response}) => {
                console.log(response);
                setHasErrors(true);
            });
        })();
    }, []);

    if(hasErrors == true) {
        return <div style={{marginTop : '10px', color : 'red'}}>Bład logowania</div>;
    }
    return <div style={{marginTop : '10px'}}>Autoryzacja - proszę czekać</div>;
}

export default Authenticate;