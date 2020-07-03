import React from 'react';
import {useRoutes} from 'hookrouter';
import Home from './modules/Home/Home';
import Footer from './modules/Global/Footer';
import Header from './modules/Global/Header';
import Resources from './modules/Resources/Resources';
import Finances from './modules/Finances/Finances';
import Transactions from './modules/Transactions/Transactions';
import Account from './modules/Account/Account';
import Login from './modules/Account/Login';
import Register from './modules/Account/Register';
import Categories from './modules/Categories/Categories';
import Rate from './modules/Rate/Rate';
import Settings from './modules/Settings/Settings';
import NotFound from './modules/Error/NotFound';

function getCookie(name) {
  var nameEQ = name + "=";
  var ca = document.cookie.split(';');
  for(var i=0;i < ca.length;i++) {
      var c = ca[i];
      while (c.charAt(0)==' ') c = c.substring(1,c.length);
      if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
  }
  return null;
}

function eraseCookie(name) {   
  document.cookie = name+'=; Max-Age=-99999999;';  
}

function redirect(path) {
  window.location.href = path;
}

function App() {

  let token = getCookie('PiggyBank.UserToken');
  let identifier = getCookie('PiggyBank.Identifier');

  let loggedIn = token != null ? true : false;
  let titleLabel = loggedIn ? 'Error' : 'Wymagane zalogowanie';

  const routes = loggedIn ? {
    '/': () => { titleLabel = 'Pulpit'; return <Home /> },
    '/resources': () => { titleLabel = 'Rachunki'; return <Resources /> },
    '/finances': () => { titleLabel = 'Finanse'; return <Finances /> },
    '/transactions': () => { titleLabel = 'Transakcje'; return <Transactions /> },
    '/account': () => { titleLabel = 'Moje konto'; return <Account /> },
    '/categories': () => { titleLabel = 'Kategorie'; return <Categories /> },
    '/rate': () => { titleLabel = 'Oceń aplikację'; return <Rate /> },
    '/settings': () => { titleLabel = 'Ustawienia'; return <Settings /> },
    '/logout': () => { eraseCookie("PiggyBank.UserToken"); eraseCookie("PiggyBank.Identifier"); window.location.href = "/login"; }
  } : {
    '/login': () => { titleLabel = 'Zaloguj się'; return <Login /> },
    '/register': () => { titleLabel = 'Zarejestruj'; return <Register /> }
  };

  const routeResult = useRoutes(routes);
  
  const headerData = loggedIn ? [
    { name: 'Moje konto', url: '/account', icon: 'fa-user' },
    { name: 'Moje kategorie', url: '/categories', icon: 'fa-list-ul' },
    { name: 'Oceń aplikację', url: '/rate', icon: 'fa-star' },
    { name: 'Ustawienia', url: '/settings', icon: 'fa-cog' },
    { name: 'Wyloguj', url: '/logout', icon: 'fa-sign-out-alt' }
  ] : [
    { name: 'Zaloguj', url: '/login', icon: 'fa-user' },
    { name: 'Zarejestruj', url: '/register', icon: 'fa-list-ul' }
  ];

  if(loggedIn) {
    return (
      <div className="App">
      <Header data={headerData} label={titleLabel} identifier={identifier} />
        { routeResult || <NotFound status={404} /> }
        <Footer />
      </div>
    );
  } else {
    return (
      <div className="App">
      <Header data={headerData} label={titleLabel} identifier={ "Nie zalogowano" } />
        { routeResult || redirect('/login') }
      </div>
    );
  }
}

export default App;