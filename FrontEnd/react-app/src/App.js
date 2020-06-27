import React from 'react';
import {useRoutes} from 'hookrouter';
import Home from './modules/Home/Home';
import Footer from './modules/Global/Footer';
import Header from './modules/Global/Header';

const routes = {
  '/': () => <Home />
  // '/resources': () => ,
  // '/finances': () => ,
  // '/transactions': () => ,
  // '/account': () => ,
  // '/categories': () => ,
  // '/rate': () => ,
  // '/settings': () =>
};

function App() {

  const routeResult = useRoutes(routes);

  return (
    <div className="App">
      <Header />
      {routeResult}
      <Footer />
    </div>
  );
}

export default App;