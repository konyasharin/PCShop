import { useRef, useState } from 'react';
import getComponentsFromDatabase from 'api/components/getComponents.ts';

function useComponents<TResponse>(url: string) {
  const [components, setComponents] = useState<TResponse[]>([]);
  const offset = useRef(0);
  function getComponents() {
    getComponentsFromDatabase<TResponse[]>(url, 3, offset.current).then(
      response => {
        setComponents([...components, ...response.data.data]);
        offset.current = offset.current + 3;
        console.log(offset.current);
      },
    );
  }

  return { components, getComponents };
}

export default useComponents;
