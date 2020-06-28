import React from 'react';
import {useRoutes} from 'hookrouter';
import Home from './modules/Home/Home';
import Footer from './modules/Global/Footer';
import Header from './modules/Global/Header';
import Resources from './modules/Resources/Resources';
import Finances from './modules/Finances/Finances';
import Transactions from './modules/Transactions/Transactions';
import Account from './modules/Account/Account';
import Categories from './modules/Categories/Categories';
import Rate from './modules/Rate/Rate';
import Settings from './modules/Settings/Settings';
import NotFound from './modules/Error/NotFound';

/* input data + variables */
const firstName = 'Michał';
const lastName = 'Droździk';
let titleLabel = 'Error';

const routes = {
  '/': () => { titleLabel = firstName != null ? 'Witaj, ' + firstName : "Strona główna"; return <Home /> },
  '/resources': () => { titleLabel = 'Rachunki'; return <Resources /> },
  '/finances': () => { titleLabel = 'Finanse'; return <Finances /> },
  '/transactions': () => { titleLabel = 'Transakcje'; return <Transactions /> },
  '/account': () => { titleLabel = 'Moje konto'; return <Account /> },
  '/categories': () => { titleLabel = 'Kategorie'; return <Categories /> },
  '/rate': () => { titleLabel = 'Oceń aplikację'; return <Rate /> },
  '/settings': () => { titleLabel = 'Ustawienia'; return <Settings /> }
};

function App() {

  const routeResult = useRoutes(routes);

  return (
    <div className="App">
    <Header label={titleLabel} userName={`${firstName} ${lastName}`} />
      { routeResult || <NotFound status={404} /> }
      <Footer />
    </div>
  );
}

export default App;