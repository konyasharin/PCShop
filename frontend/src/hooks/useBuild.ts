import { useEffect, useState } from 'react';
import TProduct from 'types/TProduct.ts';
import componentTypes from 'enums/componentTypes.ts';
import EBuildBlockErrors from 'enums/EBuildBlockErrors.ts';
import useBorderValues from 'hooks/useBorderValues.ts';

type TUseBuildComponent = {
  currentProduct: TProduct | null;
  currentErrorType: EBuildBlockErrors;
  isActive: boolean;
};

export type TUseBuildComponents = {
  [componentType in keyof typeof componentTypes]: TUseBuildComponent;
};

export type TUseBuildError = {
  errorType: 'Error' | 'Warning';
  componentType: keyof typeof componentTypes;
  errorDescription: string;
  errorDetailedType: keyof typeof errorDetailedTypes;
};

export const errorDetailedTypes = {
  empty: 'empty',
} as const;

function useBuild() {
  const [components, setComponents] =
    useState<TUseBuildComponents>(initBuild());
  const [progressOfBuild, setProgressOfBuild] = useBorderValues(0, 0, 100);
  const [price, setPrice] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const [errors, setErrors] = useState<TUseBuildError[]>([]);

  useEffect(() => {
    calculateProgressOfBuild();
    calculatePrice();
    calculatePower();
    updateErrors();
  }, [components]);

  function initBuild() {
    let key: keyof typeof componentTypes;
    const newComponents: {
      [key: string]: TUseBuildComponent;
    } = {};
    for (key in componentTypes) {
      newComponents[key] = {
        currentProduct: null,
        currentErrorType: EBuildBlockErrors.Error,
        isActive: false,
      };
    }
    return newComponents as TUseBuildComponents;
  }

  function setComponent(
    newProduct: TProduct | null,
    componentType: keyof typeof componentTypes,
  ) {
    setComponents({
      ...components,
      [componentType]: {
        ...components[componentType],
        currentProduct: newProduct,
        currentErrorType: newProduct
          ? EBuildBlockErrors.Success
          : EBuildBlockErrors.Error,
      },
    });
  }
  function calculateProgressOfBuild() {
    let key: keyof typeof componentTypes;
    let choseProducts = 0;
    let countProducts = 0;
    for (key in components) {
      countProducts += 1;
      if (components[key].currentProduct !== null) {
        choseProducts += 1;
      }
    }
    setProgressOfBuild((choseProducts / countProducts) * 100);
  }

  function calculatePrice() {
    let key: keyof typeof componentTypes;
    let newPrice = 0;
    for (key in components) {
      newPrice += components[key].currentProduct
        ? components[key].currentProduct!.price
        : 0;
    }
    setPrice(newPrice);
  }

  function calculatePower() {
    let key: keyof typeof componentTypes;
    let sumPower = 0;
    let countComponents = 0;
    for (key in components) {
      countComponents += 1;
      sumPower += components[key].currentProduct
        ? components[key].currentProduct!.power
        : 0;
    }
    setPower(sumPower / countComponents);
  }
  function updateErrors() {
    const newErrors: TUseBuildError[] = [];
    let key: keyof typeof components;
    for (key in components) {
      if (components[key].currentProduct === null) {
        newErrors.push({
          errorType: 'Error',
          componentType: key,
          errorDescription: `Выберите компонент из списка`,
          errorDetailedType: errorDetailedTypes.empty,
        });
      }
    }
    setErrors(newErrors);
  }

  function setIsActive(
    componentType: keyof typeof componentTypes,
    newIsActive: boolean,
  ) {
    setComponents({
      ...components,
      [componentType]: {
        ...components[componentType],
        isActive: newIsActive,
      },
    });
  }

  return {
    setComponent,
    components,
    progressOfBuild,
    price,
    power,
    errors,
    setIsActive,
  };
}

export default useBuild;
