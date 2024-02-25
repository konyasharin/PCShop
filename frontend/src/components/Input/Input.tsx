import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import styles from './Input.module.css';

type InputProps = {
  value: string;
  placeholder: string;
  onChange: (newValue: string) => void;
  className?: string;
};

const Input: React.FC<InputProps> = props => {
  return (
    <input
      className={createClassNames([props.className, styles.input])}
      type="text"
      value={props.value}
      placeholder={props.placeholder}
      onChange={e => props.onChange(e.target.value)}
    />
  );
};

export default Input;
