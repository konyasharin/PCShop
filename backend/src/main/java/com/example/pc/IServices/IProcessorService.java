package com.example.pc.IServices;

import com.example.pc.Models.ProcessorModel;

import java.util.List;

public interface IProcessorService {

    List<ProcessorModel> getProcessors();
    ProcessorModel getProcessorById(Long id);
    ProcessorModel addProcessor(ProcessorModel processorModel);
    void deleteProcessorById(Long id);
}
