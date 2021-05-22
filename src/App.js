import React, { useState } from "react";

import Home from "./pages/Home";
import Navigation from "./components/Navigation";

import "./styles/global.scss";

const defaultBackground = {
  color: "pink",
  image: "none",
};

const backgroundStorage = JSON.parse(
  window.localStorage.getItem("dataBackground")
);

const initialBackground = () => {
  if (backgroundStorage) {
    return backgroundStorage;
  } else {
    window.localStorage.setItem(
      "dataBackground",
      JSON.stringify(defaultBackground)
    );

    return defaultBackground;
  }
};

export default function App() {
  const [background, setBackground] = useState(initialBackground);

  function changeBackground(dataBackground) {
    setBackground(dataBackground);
    window.localStorage.setItem(
      "dataBackground",
      JSON.stringify(dataBackground)
    );
  }

  return (
    <div
      style={{
        backgroundColor: background.color,
        backgroundImage: background.image,
        backgroundSize: "cover",
      }}
    >
      <Navigation changeBackground={changeBackground} background={background} />
      <Home />
    </div>
  );
}
