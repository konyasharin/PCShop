package com.example.pc.IServices;

import com.example.pc.Models.MotherBoardModel;
import com.example.pc.dto.MotherBoardDto;

import java.util.List;

public interface IMotherBoardService {

    List<MotherBoardModel> getMotherBoards();
    MotherBoardModel getMotherBoardById(Long id);
    MotherBoardModel updateMotherBoard(MotherBoardModel motherBoardModel);
    void deleteMotherBoardById(Long id);
    MotherBoardModel createMotherBoard(MotherBoardDto motherBoardDto);
}
