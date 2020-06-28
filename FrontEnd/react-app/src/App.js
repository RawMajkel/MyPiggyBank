import React from 'react';
import {useRoutes} from 'hookrouter';
import Home from './modules/Home/Home';
import Footer from './modules/Global/Footer';
import Header from './modules/Global/Header';
import Resources from './modules/Resources/Resources';

/* input data + variables */
const firstName = 'Michał';
const lastName = 'Droździk';
let titleLabel;

const routes = {
  '/': () => { titleLabel = firstName != null ? 'Witaj, ' + firstName : "Strona główna"; return <Home /> },
  '/resources': () => { titleLabel = 'Rachunki'; return <Resources /> },
  '/finances': () => { titleLabel = 'Finanse'; return <Finances /> },
  '/transactions': () => { titleLabel = 'Transakcje'; return <Transactions /> },
  // '/account': () => ,
  // '/categories': () => ,
  // '/rate': () => ,
  // '/settings': () =>
};

function App() {

  const routeResult = useRoutes(routes);

  return (
    <div className="App">
    <Header label={titleLabel} userName={`${firstName} ${lastName}`} />
      {routeResult}
      <Footer pathName={window.location.pathname} />
    </div>
  );
}

export default App;