import styles from './SearchWindowBlock.module.css';
import React from 'react';

type SearchWindowBlockProps = {
  img: string;
  title: string;
  text: string;
  isActive: boolean;
};

const SearchWindowBlock: React.FC<SearchWindowBlockProps> = props => {
  return (
    <div
      className={
        props.isActive ? `${styles.block} ${styles.blockActive}` : styles.block
      }
    >
      <img src={props.img} alt="elem" />
      <div>
        <h6>{props.title}</h6>
        <span>{props.text}</span>
      </div>
    </div>
  );
};

export default SearchWindowBlock;
