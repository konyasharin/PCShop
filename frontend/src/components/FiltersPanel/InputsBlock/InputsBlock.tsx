import React from 'react';
import styles from './InputsBlock.module.css';
import createClassNames from 'utils/createClassNames.ts';

type InputsBlockProps = {
  inputs: {
    name: string;
    placeholder: string;
    value: string;
  }[];
  onChange: (name: string, newValue: string) => void;
  inputClassName?: string;
};

const InputsBlock: React.FC<InputsBlockProps> = props => {
  return (
    <>
      {...props.inputs.map(input => {
        return (
          <input
            type={'number'}
            placeholder={input.placeholder}
            onChange={e => props.onChange(input.name, e.target.value)}
            value={input.value}
            className={createClassNames([props.inputClassName, styles.input])}
          />
        );
      })}
    </>
  );
};

export default InputsBlock;
