import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import styles from './Input.module.css';

export type InputProps = {
  value: string;
  placeholder: string;
  onChange: (newValue: string) => void;
  className?: string;
  type?: 'number' | 'text';
};

const Input: React.FC<InputProps> = props => {
  return (
    <input
      className={createClassNames([styles.input, props.className])}
      type={props.type ? props.type : 'text'}
      value={props.value}
      placeholder={props.placeholder}
      onChange={e => props.onChange(e.target.value)}
    />
  );
};

export default Input;
