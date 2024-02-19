package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class ProcessorDto {

    private String brand;

    private String model;

    private Integer price;

    private String description;

    private String country;

    private Integer clock_frequency;

    private Integer turbo_frequency;

    private Integer heat_dissipation;

    private MultipartFile image;

}
