package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class MotherBoardDto {

    private String brand;

    private String model;

    private Integer price;

    private String description;

    private String country;

    private Integer frequency;

    private String socket;

    private String chipset;

    private MultipartFile image;
}
