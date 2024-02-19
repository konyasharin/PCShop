package com.example.pc.IServices;

import com.example.pc.Models.ProcessorModel;
import com.example.pc.dto.ProcessorDto;

import java.util.List;

public interface IProcessorService {

    List<ProcessorModel> getProcessors();
    ProcessorModel getProcessorById(Long id);
    ProcessorModel updateProcessor(ProcessorModel processorModel);
    void deleteProcessorById(Long id);
    ProcessorModel createProcessor(ProcessorDto processorDto);
}
