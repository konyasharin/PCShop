import styles from '../../pages/AdminPage/Edit/EditProductInfo/EditProductInfo.module.css';
import React from 'react';

type SelectImgProps = {
  setImg: (imgFile: File) => void;
  img: string;
  className?: string;
};

const SelectImg: React.FC<SelectImgProps> = props => {
  return (
    <>
      <input
        type="file"
        id={'editProductImg'}
        className={styles.inputImg}
        onChange={e => {
          if (e.target.files) {
            props.setImg(e.target.files[0]);
          }
        }}
        accept={'image/*'}
      />
      <label htmlFor="editProductImg">
        <img
          src={props.img}
          alt="componentImg"
          className={props.className ? props.className : ''}
        />
      </label>
    </>
  );
};

export default SelectImg;
