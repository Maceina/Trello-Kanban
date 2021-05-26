import React from "react";
import { BarChart } from "@material-ui/icons";

import "./styles.scss";

export default function Topbar({ setOpenSideMenu }) {
  return (
    <div>
      <nav>
        <div className="container">
          <div>
            <BarChart />
            <h1>Trello</h1>
          </div>
          <button onClick={() => setOpenSideMenu(true)}>Background</button>
        </div>
      </nav>
    </div>
  );
}
