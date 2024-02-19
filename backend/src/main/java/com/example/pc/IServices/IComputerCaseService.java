package com.example.pc.IServices;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.dto.ComputerCaseDto;

import java.util.List;

public interface IComputerCaseService {

    List<ComputerCaseModel> getAllComputerCases();
    ComputerCaseModel getComputerCaseById(Long id);
    ComputerCaseModel updateComputerCase(ComputerCaseModel computerCaseModel);
    void deleteComputerCaseById(Long id);
    ComputerCaseModel createComputerCase(ComputerCaseDto computerCaseDto);
}
