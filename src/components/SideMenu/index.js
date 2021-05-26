import React, { useEffect, useState } from "react";
import { Drawer, Grow } from "@material-ui/core";

import colors from "../../utils/colors";
import { getImages } from "../../utils/imageApi";

import "./styles.scss";

export default function SideMenu({
  openSideMenu,
  setOpenSideMenu,
  changeBackground,
  background,
}) {
  const [openOptionColor, setOpenOptionColor] = useState(false);
  const [openOptionImage, setOpenOptionImage] = useState(false);
  const [images, setImages] = useState([]);

  const getListOfImages = async () => {
    const listOfImages = await getImages();
    setImages(listOfImages);
  };

  useEffect(() => {
    getListOfImages();
  }, []);

  return (
    <div>
      <Drawer
        open={openSideMenu}
        anchor="right"
        onClose={() => setOpenSideMenu(false)}
      >
        <div className="drawer">
          <div className="menu">
            <div
              className="background-box"
              style={{
                backgroundImage: `url('https://live.staticflickr.com/65535/33792125018_8e6a234766_b.jpg')`,
                backgroundSize: "cover",
                backgroundRepeat: "no-repeat",
              }}
              onClick={() => {
                setOpenOptionColor(false);
                setOpenOptionImage(true);
              }}
            >
              Image
            </div>
            <div
              className="background-box"
              style={{
                backgroundImage:
                  "url('https://htmlcolorcodes.com/assets/images/html-color-codes-color-palette-generators.jpg')",
                backgroundSize: "contain",
              }}
              onClick={() => {
                setOpenOptionImage(false);
                setOpenOptionColor(true);
              }}
            >
              Color
            </div>
          </div>

          {openOptionImage ? (
            <Grow in={openOptionImage}>
              <div className="option-container">
                {images.map((image, index) => {
                  return (
                    <div
                      className="background-box"
                      style={{
                        backgroundImage: `url(${image.thumb})`,
                        backgroundSize: "cover",
                        backgroundRepeat: "no-repeat",
                      }}
                      key={index}
                      onClick={() =>
                        changeBackground({
                          color: background.color,
                          image: `url(${image.full})`,
                        })
                      }
                    ></div>
                  );
                })}
              </div>
            </Grow>
          ) : (
            <Grow in={openOptionColor}>
              <div className="option-container">
                {colors.map((color, index) => {
                  return (
                    <div
                      className="background-box"
                      style={{
                        background: color,
                      }}
                      key={index}
                      onClick={() =>
                        changeBackground({
                          color: color,
                          image: "none",
                        })
                      }
                    >
                      {color}
                    </div>
                  );
                })}
              </div>
            </Grow>
          )}
        </div>
      </Drawer>
    </div>
  );
}
