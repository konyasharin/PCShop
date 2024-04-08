import React from 'react';
import Input, { InputProps } from 'components/inputs/Input/Input.tsx';
import styles from './SearchInput.module.css';
import searchImg from 'assets/search.svg';
import createClassNames from 'utils/createClassNames.ts';

type SearchInputProps = InputProps & {
  onSearch: () => void;
  className?: string;
};

const SearchInput: React.FC<SearchInputProps> = props => {
  return (
    <div className={createClassNames([styles.inputBlock, props.className])}>
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
