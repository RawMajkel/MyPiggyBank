import React from 'react';

function NotFound() {
  return (
    <div className="section">
      <div className="container">
        <span className="latest__heading heading d-block font-weight-600">Error 404 - Nie znaleziono strony</span>
        <p>Szukasz czegoś, co nie istnieje - z pustego i Salomon nie naleje!</p>
        <a href="/" className="button d-inline-flex align-items-center justify-content-start color-white">Wróc do strony głównej</a>
      </div>
    </div>
  );
}

export default NotFound;