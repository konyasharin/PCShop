import React from 'react';
import Input, { InputProps } from 'components/inputs/Input/Input.tsx';
import styles from './SearchInput.module.css';
import searchImg from 'assets/search.svg';

type SearchInputProps = InputProps & {
  onSearch: () => void;
};

const SearchInput: React.FC<SearchInputProps> = props => {
  return (
    <div className={styles.inputBlock}>
      <Input
        value={props.value}
        placeholder={props.placeholder}
        onChange={props.onChange}
        type={props.type}
      />
      <img
        src={searchImg}
        alt="search"
        onClick={props.onSearch}
        className={styles.searchImg}
      />
    </div>
  );
};

export default SearchInput;
