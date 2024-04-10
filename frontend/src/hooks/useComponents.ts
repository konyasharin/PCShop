import { useEffect, useState } from 'react';
import getComponentsFromDatabase from 'api/components/getComponents.ts';
import componentTypes from 'enums/componentTypes.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';

export type TUseComponents = {
  [key in keyof typeof componentTypes]: {
    components: TOneOfComponents<string>[];
    offset: number;
  };
};

/**
 * Хук для хранения всех подгруженных компонентов
 */
function useComponents() {
  const [components, setComponents] = useState<TUseComponents | null>(null);
  const dispatch = useDispatch();

  useEffect(() => {
    async function initComponents() {
      let key: keyof typeof componentTypes;
      const newComponents: {
        [key: string]: {
          components: TOneOfComponents<string>[];
          offset: number;
        };
      } = {};
      for (key in componentTypes) {
        newComponents[key] = {
          components: (await getComponents(key)).data.data,
          offset: 3,
        };
      }
      return newComponents as TUseComponents;
    }
    dispatch(setIsLoading(true));
    initComponents().then(newComponents => {
      setComponents(newComponents);
      dispatch(setIsLoading(false));
    });
  }, []);

  async function getComponents(componentType: keyof typeof componentTypes) {
    const response = await getComponentsFromDatabase(
      componentType,
      3,
      components ? components[componentType].offset : 0,
    );
    if (components) {
      setComponents({
        ...components,
        [componentType]: {
          components: [
            ...components[componentType].components,
            ...response.data.data,
          ],
          offset: components[componentType].offset + 3,
        },
      });
    }
    return response;
  }

  return { components, getComponents };
}

export default useComponents;
