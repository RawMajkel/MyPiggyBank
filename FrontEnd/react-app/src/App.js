import React from 'react';
import {useRoutes} from 'hookrouter';
import Home from './modules/Home/Home';
import Footer from './modules/Global/Footer';
import Header from './modules/Global/Header';
import Resources from './modules/Resources/Resources';

const routes = {
  '/': () => <Home />,
  '/resources': () => <Resources />
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
      <Footer pathName={window.location.pathname} />
    </div>
  );
}

export default App;