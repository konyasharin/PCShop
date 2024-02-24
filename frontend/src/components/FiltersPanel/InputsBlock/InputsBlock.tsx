import React from 'react';

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
            placeholder={input.placeholder}
            onChange={e => props.onChange(input.name, e.target.value)}
            value={input.value}
            className={props.inputClassName ? props.inputClassName : ''}
          />
        );
      })}
    </>
  );
};

export default InputsBlock;
