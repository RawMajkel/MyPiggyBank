import React from 'react';
import Operations from '../Operations/Operations';

function Home({token}) {
  return (
    <div className="latest section">
      <div className="container">
        <span className="latest__heading heading d-block font-weight-600">Ostatnie operacje:</span>
        <Operations token={token} limit={10} />
      </div>
    </div>
  );
}

export default Home;