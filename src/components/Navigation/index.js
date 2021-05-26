import React, { useState } from "react";
import Topbar from "../Topbar";
import SideMenu from "../SideMenu";

export default function Navigation({ changeBackground, background }) {
  const [openSideMenu, setOpenSideMenu] = useState(false);

  return (
    <div>
      <Topbar setOpenSideMenu={setOpenSideMenu} />
      <SideMenu
        openSideMenu={openSideMenu}
        setOpenSideMenu={setOpenSideMenu}
        changeBackground={changeBackground}
        background={background}
      />
    </div>
  );
}
